/// <reference path="jquery-3.1.1.js" />
function init(schema, callback) {
    $('body>header>div.r>input').val(schema.ServerUrl);
    var baseType = "byte,short,int,long,float,double,decimal,boolean,datetime,datetimeoffset,guid,string,object";
    var isBaseType = function (input) {
        return baseType.indexOf(input.toLowerCase()) > -1;
    };
    $.each(schema.ResourceInfos, function (i, info) {
        var dd = $('<dd><span class="name">' + info.Name + '</span>: <cite class="' + (info.Type.IsBaseType ? 'type' : 'class') + '">' + info.Type.Name + '[]</cite></dd>');
        $('body>main>div.l>dl#resources').append(dd);
    });
    $.each(schema.ResourceTypes, function (i, type) {
        var dd = $('<dd></dd>');
        dd.append('<div><span class="class">' + type.Name + '</span></div>');
        dd.append('<div>{</div>');
        $.each(type.Properties, function (j, member) {
            dd.append('<div>&nbsp;&nbsp;&nbsp;&nbsp;<span class="name">' + member.Name + '</span>: <cite class="' + (member.Type.IsBaseType ? 'type' : 'class') + '">' + member.Type.Name + '</cite></div>');
        });
        dd.append('<div>}</div>');
        $('body>main>div.l>dl#types').append(dd);
    });
    $.each(schema.ResourceMethods, function (i, method) {
        var dd = $('<dd></dd>');
        dd.append('<div><span class="class">' + method.Name + '</span>' + (method.Inherit ? ': <span class="class">' + method.Inherit + '</span>' : '') + '</div>');
        dd.append('<div>{</div>');
        $.each(method.Methods, function (j, member) {
            var parameters = '';
            $.each(member.Parameters, function (k, parameter) {
                parameters += ',<span class="name">' + parameter.Name + '</span>' + (parameter.IsOptional ? '<cite class="optional">?</cite>' : '') + ': <cite class="' + (parameter.Type.IsBaseType ? 'type' : 'class') + '">' + parameter.Type.Name + '</cite>';
            });
            dd.append('<div>&nbsp;&nbsp;&nbsp;&nbsp;<span class="name">' + member.Name + '</span> (<span class="parameters">' + parameters.substr(1) + '</span>): <cite class="' + (member.ReturnType.IsBaseType ? 'type' : 'class') + '">' + member.ReturnType.Name + '</cite></div>');
        });
        dd.append('<div>}</div>');
        $('body>main>div.l>dl#methods').append(dd);
    });
    callback && callback();
}
function send(body, header) {
    var timer, time;
    $('body>main>div.r>div.m button').attr('disabled', 'disabled');
    $('body>main>div.r>div.m #time').text('0.0s');
    $.ajax($('body>header>div.r>input').val(), {
        type: 'GET',
        data: body,
        cache: false,
        contentType: 'text/plain',
        dataType: 'json',
        beforeSend: function (xhr) {
            $.each(header, function (key, value) {
                xhr.setRequestHeader(key, value);
            });
            time = new Date();
            timer = setInterval(function () {
                $('body>main>div.r>div.m #time').text(((new Date() - time) / 1000).toFixed(2) + 's');
            }, 75);
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON)
                $('body>main>div.r>div.b textarea').val(JSON.stringify(xhr.responseJSON, null, 4));
            else
                $('body>main>div.r>div.b textarea').val(error || status);
        },
        success: function (response, status, xhr) {
            $('body>main>div.r>div.b textarea').val(JSON.stringify(response, null, 4));
        },
        complete: function (e, xhr) {
            $('body>main>div.r>div.m #status').text(e.status);
            $('body>main>div.r>div.m button').removeAttr('disabled');
            clearInterval(timer);
        }
    });
}
$(function () {
    $.getJSON('schema.json?v=1.1.6', function (schema) {
        init(schema || {}, function () {
            $('body>main>div.l>dl>dt').click(function () {
                $(this).find('span').text($(this).find('span').text() === '+' ? '-' : '+');
                $(this).parent().find('dd').slideToggle();
            });
            $('body>main>div.r>div.t textarea').on('keydown', function (e) {
                if (e.keyCode === 9) {
                    e.preventDefault && e.preventDefault();
                    var str = '    ';
                    var obj = $(this)[0];
                    if (document.selection) {
                        var sel = document.selection.createRange();
                        sel.text = str + sel.text;
                    } else if (typeof obj.selectionStart === 'number' && typeof obj.selectionEnd === 'number') {
                        var startPos = obj.selectionStart,
                            endPos = obj.selectionEnd,
                            cursorPos = startPos,
                            tmpStr = obj.value;
                        obj.value = tmpStr.substring(0, startPos) + str + tmpStr.substring(startPos);
                        cursorPos += str.length;
                        obj.selectionStart = obj.selectionEnd = cursorPos;
                    } else {
                        obj.value += str;
                    }
                }
            });
            $('body>main>div.r>div.m input').on('blur', function () {
                if ($(this).val() === '')
                    $(this).val('query');
            });
            $('body>main>div.r>div.m button').click(function () {
                var data = {};
                data[$('body>main>div.r>div.m input#query').val()] = $('body>main>div.r>div.t textarea').val().replace(/\s/g, '');
                if ($(this).is('#sendInQuery'))
                    send(data, null);
                else if ($(this).is('#sendInHeader'))
                    send({ sendInHeader: true }, data);
            });
        });
    });
});