/*
*name:manageCom
*author:wenqing
*封装一些后台管理模块中公共js
*/
; layui.define(function (exports) {
    var $ = layui.jquery;
    var manageCom = {
        getHeight: function() {
            return $(window).height() -
                $('fieldset').outerHeight(true) -
                $('.mytool').outerHeight(true);
        },
        selectInint: function (selectName/*name选择器*/, data/*enumData.js数据*/, defaulVal/*默认值*/) {
           
            data.forEach(function (item, index) {
                console.info(!!defaulVal);
                $("select[name='" + selectName+"']").append("<option value=" + item.value + ">" + item.text + "</option>");
            });
            //赋值
            if (!!defaulVal) {
                $("select[name='" + selectName + "']").val(defaulVal);
            }
        }, 
        getUrlParam:function (name){
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        },
        //Ajax 文件下载
        download:function (url, data, method) {
            // 获取url和data
            if (url && data) {
                // data 是 string 或者 array/object
                data = typeof data == 'string' ? data : $.param(data);
                // 把参数组装成 form的  input
                var inputs = '';
                $.each(data.split('&'), function () {
                    var pair = this.split('=');
                    inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                });
                // request发送请求
                $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>')
                    .appendTo('body').submit().remove();
            };
        },
        submitForbid:function(obj) {
            var submitId = obj;
            submitId.disabled = true;
            setTimeout("submitId.disabled=false;", 3000); //代码核心在这里，3秒还原按钮代码
        }

}
    exports('manageCom', manageCom);
});


