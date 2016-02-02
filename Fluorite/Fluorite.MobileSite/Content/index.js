﻿$(document).ready(function () {
    $('div.left').hide();
   
    var windowWidth = $(window).width();
    var mainWidth = (windowWidth * 166 / 100)+5;
    $('div.main').css('width', mainWidth + 'px');
//    console.log(mainWidth);
    
    var setResponsiveValue = function ($obj, values, propertyNames) {
        if (propertyNames && values) {
            var finalValue = 0;
            for (var i = 0; i < propertyNames.length; i++) {
                if (values[i]!==undefined) {
                    finalValue = (values[i] * windowWidth) / 320;
                }
                $obj.css(propertyNames[i], finalValue + 'px');
            }
        }
    }
    setResponsiveValue($('.loading'), [200,120], ['top','left']);
    setResponsiveValue($('body'), [15], ['font-size']);
    setResponsiveValue($('div.head'), [5], ['padding-top']);
    setResponsiveValue($('div.menu'), [5], ['padding-top', 'padding-left']);
    setResponsiveValue($('div.menu img'), [20], ['height']);
    setResponsiveValue($('div.logo img'), [32],['height']);
    setResponsiveValue($('div.item_text'), [50,45], ['height','line-height']);
    setResponsiveValue($('div.foot_right_text'), [55,47.5], ['height', 'line-height']);
    setResponsiveValue($('div.foot_logo'), [55], ['min-height']);
    setResponsiveValue($('.left_search'), [40], ['height']);
    setResponsiveValue($('.left_search_input'), [30], ['height']);
    setResponsiveValue($('#search'), [30], ['height']);
    setResponsiveValue($('.left_menu'), [40], ['height', 'line-height']);
    setResponsiveValue($('.search_button'), [30], ['height']);
    setResponsiveValue($('.search_button img'), [3], ['margin-top']);
    setResponsiveValue($('div.content'), [15], ['margin-top', 'margin-bottom']);
    setResponsiveValue($('div.item'), [10], ['margin-top', 'margin-bottom']);

    $('.loading').hide();
    $('.main').show();

    $('div.left').css('height', '2000px');
    $('div.cover').css('width', '2000px').css('height', '2000px').css('left', (mainWidth - $('.right').width()) + 'px');

    var headHeight = $('.head').height();
    var headPaddingTop = (5 * windowWidth) / 320;
    $('.under_head').css('height', (headHeight + headPaddingTop) + 'px');

    var owlItemWidth;
    $(".owl-carousel").owlCarousel({
        autoplay: true,
        items: 1,
        loop: true,
        dots: true,
        onInitialized: function () {
            setResponsiveValue($('.owl-dots'), [120, 20], ['width', 'bottom']);
            setResponsiveValue($('.owl-dots span'), [3], ['margin-left', 'margin-right']);
            owlItemWidth = $('.owl-item').eq(0).width() - 5;
            $('.owl-stage-outer').css('width', owlItemWidth + 'px');
        }
    });

    setTimeout(function () {
        var owlStageOuterHeight = $('.owl-stage-outer').height();
        $('.owl-carousel').css('width', owlItemWidth + 'px').css('height', owlStageOuterHeight + 'px').css('margin', '0 auto');
        $('.owl-stage').css('right', '2px');

    }, 500);

    var leftShow = false;
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

    $('.left').on('touchmove', function (event) {
        var e = window.event || event;
        e.preventDefault();
        e.returnValue = false;
    });
});


    
    

    