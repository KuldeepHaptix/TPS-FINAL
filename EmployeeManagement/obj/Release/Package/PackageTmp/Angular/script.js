function getUrlParameter(param, dummyPath) {
    var sPageURL = dummyPath || window.location.search.substring(1),
        sURLVariables = sPageURL.split(/[&||?]/),
        res;
    for (var i = 0; i < sURLVariables.length; i += 1) {
        var paramName = sURLVariables[i],
            sParameterName = (paramName || '').split('=');

        if (sParameterName[0] === param) {
            res = sParameterName[1];
        }
    }
    return res;
}
function setCookie(key, value) {
    var expires = new Date();
    expires.setTime(expires.getTime() + 31536000000); //1 year
    document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
}
function getCookie(key) {
    var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
    return keyValue ? keyValue[2] : null;
}
function openDropdown(selector) {
    $(".mdl-menu__container").addClass("is-visible");
}
function btn_setting() {
    //alert("click");
    $(".mdl-menu__container").toggleClass("is-visible");
}
var UrlLink = 'http://testapp.vayuna.com/service.asmx/';

var NoDataMesg = 'No Data Available';

$(document).mouseup(function (e) {
    var container = $(".mdl-menu__container");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.removeClass("is-visible");
    }
});