function AjaxRequest(url,data,callBack) {
    $.ajax({
        type: 'post',
        dataType: 'json',
        url: url,
        data: data,
        cache: false,
        async: false,
        success: function (res) {
            if (res != null) {
                callBack(res);
            }
            else {
                parent.window.location.href = "Error.html";
            }
        }
    });
}
function AjaxLoadChildPage(url) {
    jAlertLoader("页面加载中......");
    $("#RightContent").empty();
    $("#RightContent").load(url, function (response, status) {
        if (status == "success") {
            //$("select,input:checkbox,input:radio,input:file").uniform();
            jRemoveLoader();
        }
    });
}

function PostBackUrl(url) {
    $("#HideUrl").val(url);
    AjaxLoadChildPage(url);
}