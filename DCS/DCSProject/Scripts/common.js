var siteurl = $("#hdSiteUrl").val();
var serverurl=window.location.href;
if (serverurl.indexOf('/DCSPP/')>0) {
    siteurl += "DCSPP/"
} 

$("#lnkSignup").click(function () {
    $("iframe#frmLogin").attr("src", '');
    $('.modal-title').html('Login Or Registration');
    url = siteurl + 'account/register';
    $("iframe#frmLogin").attr("src", url);
    $('#myModal').modal('show');
    return false;
});

$('body').on('click', '.btnaddProjectOwnerNameANTM', function () {
    var parentid = $(this).attr("data-parent");
    $("iframe#frmAction").attr("src", '');
    $('.modal-title').html('Add Anthem Owner');
    url = siteurl + 'project/AddProjectOwnerNameANTM';
    $("iframe#frmAction").attr("src", url);
    $('#myModal').modal('show');
    return false;
});

$('body').on('click', '.btnaddProjectOwnerNameExternal', function () {
    var parentid = $(this).attr("data-parent");
    $("iframe#frmAction").attr("src", '');
    $('.modal-title').html('Add Project Co-Owner/ Partner');
    url = siteurl + 'project/addProjectOwnerNameExternal';
    $("iframe#frmAction").attr("src", url);
    $('#myModal').modal('show');
    return false;
});

$('body').on('click', '.btnCreateETOPlaybook', function () {
    var parentid = $(this).attr("data-parent");
    $("iframe#frmAction").attr("src", '');
    $('.modal-title').html('Create ETO Playbook');
    url = siteurl + 'ETOPlaybook/CreateETOPlaybook';
    $("iframe#frmAction").attr("src", url);
    $('#myModal').modal('show');
    return false;
});

$('body').on('click', '.btneditProjectOwnerNameANTM', function () {
    var parentid = $(this).attr("data-parent");
    $("iframe#frmAction").attr("src", '');
    $('.modal-title').html('Edit Anthem Owner');
    url = siteurl + 'project/EditProjectOwnerNameANTM';
    $("iframe#frmAction").attr("src", url);
    $('#myModal').modal('show');
    return false;
});

$('body').on('click', '.btneditProjectOwnerNameExternal', function () {
    var parentid = $(this).attr("data-parent");
    $("iframe#frmAction").attr("src", '');
    $('.modal-title').html('Edit Project Co-Owner/ Partner');
    url = siteurl + 'project/EditProjectOwnerNameExternal';
    $("iframe#frmAction").attr("src", url);
    $('#myModal').modal('show');
    return false;
});


function hidepopup() {
    $('#myModal').modal('hide');
}

function hidepopup2() {
    $('#myModal2').modal('hide');
}

function hideCitypopup() {
    $('#myCityPopupModal').modal('hide');
    window.location.href = window.location.href;
}

function hidepopupWithlocationReload(url) {
    $('#myModal').modal('hide');
    var redirecturl = url;
    if (url != null && url != '') {
        url = '';
        window.location.href = redirecturl;
    }
    else {
        window.location.href = window.location.href;
    }

}

if (navigator.userAgent.match(/MSIE\s(?!10.0)/)) {
    //console.log("ie less than version 10")
    placeholder = function () {
        $('input, textarea').each(function () {
            var holder = $(this).attr('placeholder');
            if (typeof (holder) !== 'undefined') {
                $(this).val(holder);
                $(this).bind('click', function () {
                    $(this).val('');
                }).blur(function () {
                    $(this).val(holder);
                });
            }
        });
    };
    $(document).ready(placeholder);
}


function isDate(txtDate) {
    var objDate,  // date object initialized from the txtDate string
        mSeconds, // txtDate in milliseconds
        day,      // day
        month,    // month
        year;     // year
    // date length should be 10 characters (no more no less)
    //var 
    if (txtDate.length !== 10) {
        return false;
        alert("Please select Date in Valid form.");
    }
    // third and sixth character should be '/'
    if (txtDate.substring(2, 3) !== '/' || txtDate.substring(5, 6) !== '/') {
        return false;
    }
    // extract month, day and year from the txtDate (expected format is mm/dd/yyyy)
    // subtraction will cast variables to integer implicitly (needed
    // for !== comparing)
    day = txtDate.substring(3, 5) - 1; // because months in JS start from 0
    month = txtDate.substring(0, 2) - 0;
    year = txtDate.substring(6, 10) - 0;
    // test year range
    if (year < 1000 || year > 3000) {
        console.log(year);
        return false;
    }
    // convert txtDate to milliseconds
    mSeconds = (new Date(year, month, day)).getTime();
    // initialize Date() object from calculated milliseconds
    objDate = new Date();
    objDate.setTime(mSeconds);
    // compare input date and parts from Date() object
    // if difference exists then date isn't valid
    if (objDate.getFullYear() !== year ||
        objDate.getMonth() !== month ||
        objDate.getDate() !== day) {
        return false;
    }
    // otherwise return true
    return true;
}

function RequestQueryString(stringName) {
    var i = 0;
    var t = window.location.search.slice(1).split('&');
    for (i = 0; i < t.length; i++) {
        var qrystr = t[i].split('=');
        if (qrystr[0] == stringName) {
            return qrystr[1];
        }
    }
    return "";
}

function setCookie(cname, cvalue) {
    console.log(cvalue);
    var d = new Date();
    d.setTime(d.getTime() + (60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue.replace(/\n/g, "~") + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function checkCookie(c) {
    var user = getCookie(c);
    if (user != "") {
        return true;
    } else {
        return false;
    }
}


//for Sortorder
function isNumericSortorder(keyCode) {
    var r = ((keyCode >= 48 && keyCode <= 57) || keyCode == 8)
    return r;
}

function isNumericValue(keyCode) {
    var r = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || keyCode == 8 || keyCode == 9)
    return r;
}



function isAlphaNumericKey(event) {
    if (event.shiftKey)
        return false;
    var keyCode = event.which;
    if (!((keyCode > 47 && keyCode < 90) || (keyCode > 95 && keyCode < 106) || keyCode == 08 || keyCode == 9 || keyCode == 46)) {
        event.preventDefault();
    }
}

function isNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31
    && (charCode < 48 || charCode > 57)) {
        return false;
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
