$(document).ready(function () {
    $('div.left').hide();
    var leftShow = false;
    var windowWidth = $(window).width();
    var windowHeight = $(window).height();
    var mainWidth = windowWidth * 166 / 100;
    $('div.main').css('width', mainWidth + 'px');
    var rightWidth = $('.right').width();
    $('div.left').css('height', windowHeight + 200 + 'px');
    $('div.cover').css('width', windowWidth / 3 + 'px').css('height', windowHeight + 'px').css('left', (mainWidth - rightWidth) + 'px');
    var setHeight = function ($obj, value, propertyNames) {
        var finalValue = (value * windowWidth) / 320;
        if (propertyNames) {
            for (var i = 0; i < propertyNames.length; i++) {
                $obj.css(propertyNames[i], finalValue + 'px');
            }
        }
    }
    setHeight($('div.logo img'), 32,['height']);
    setHeight($('div.item_text'), 50, ['height','line-height']);
    setHeight($('div.foot_right_text'), 55, ['height', 'line-height']);
    setHeight($('div.foot_logo'), 55, ['min-height']);
    setHeight($('.left_search'), 40, ['height']);
    setHeight($('.left_search_input'), 30, ['height']);
    setHeight($('.left_search_input input'), 30, ['height']);
    setHeight($('.left_menu'), 40, ['height', 'line-height']);
    setHeight($('.search_button'), 30, ['height']);
    setHeight($('div.content'), 15, ['margin-top', 'margin-bottom']);
    setHeight($('div.item'), 10, ['margin-top', 'margin-bottom']);

    var showLeftMenu = function () {
        window.scrollTo(0, 0);
        $('div.left').show();
        $('div.cover').show();
        leftShow = true;
    };
    var hideLeftMenu = function () {
        $('div.left').hide();
        $('div.cover').hide();
        leftShow = false;
    };
    $('div.menu').on('touchend', function (event) {
        if (leftShow) {
            hideLeftMenu();
        } else {
            showLeftMenu();
        }
        var e = window.event || event;
        e.preventDefault();
        e.returnValue = false;
    });
    $('.cover').on('touchstart', hideLeftMenu);
    $(".owl-carousel").owlCarousel({
        autoplay: true,
        items: 1,
        loop: true,
        dots: true
    });
    $('.left').on('touchmove', function (event) {
        var e = window.event || event;
        e.preventDefault();
        e.returnValue = false;
    });
});