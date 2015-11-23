$.fn.extend({
    equalHeights: function () {
        var top = 0;
        var row = [];
        var classname = ('equalHeights' + Math.random()).replace('.', '');
        $(this).each(function () {
            var thistop = $(this).offset().top;
            if (thistop > top) {
                $('.' + classname).removeClass(classname);
                top = thistop;
            }
            $(this).addClass(classname);
            $(this).height('auto');
            var h = (Math.max.apply(null, $('.' + classname).map(function () { return $(this).outerHeight(); }).get()));
            $('.' + classname).height(h);
        }).removeClass(classname);
    }
});

$(function () {
    $(window).resize(function () {
        $('.dvsearchuserlist .col-md-4.col-sm-6').equalHeights();
        $('.homecitylist .col-md-4.col-sm-6').equalHeights();
        $('.dvcurrentcitypopup .col-xs-4.col-md-4.col-sm-4').equalHeights();
    }).trigger('resize');
});

$(document).ready(function () {
    $('.dvsearchuserlist .col-md-4.col-sm-6').equalHeights();
    $('.homecitylist .col-md-4.col-sm-6').equalHeights();
    $('.dvcurrentcitypopup .col-xs-4.col-md-4.col-sm-4').equalHeights();
});
