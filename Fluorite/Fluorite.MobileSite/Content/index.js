$(document).ready(function () {
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
    setResponsiveValue($('div.item_text'), [45,40], ['height','line-height']);
    setResponsiveValue($('div.foot_right_text'), [55,45], ['height', 'line-height']);
    setResponsiveValue($('div.foot_logo'), [55], ['min-height']);
    setResponsiveValue($('.left_search'), [40], ['height']);
    setResponsiveValue($('.left_search_input'), [30], ['height']);
    setResponsiveValue($('#search'), [25], ['height']);
    setResponsiveValue($('.left_menu'), [40], ['height', 'line-height']);
    setResponsiveValue($('.search_button'), [30], ['height']);
    setResponsiveValue($('.search_button img'), [6], ['top']);
    setResponsiveValue($('div.content'), [15], ['margin-top', 'margin-bottom']);
    setResponsiveValue($('div.item'), [15], ['margin-top', 'margin-bottom']);
//    setResponsiveValue($('div.right_main'), [1], ['right']);
    

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
        smartSpeed: 500,
        autoplayTimeout: 5000,
        onInitialized: function () {
            setResponsiveValue($('.owl-dots'), [120, 20], ['width', 'bottom']);
            setResponsiveValue($('.owl-dots span'), [3], ['margin-left', 'margin-right']);
//            owlItemWidth = $('.owl-item').eq(0).width()-5;
//            $('.owl-stage-outer').css('width', owlItemWidth + 'px');
        }
    });

    setTimeout(function () {
//        var owlStageOuterHeight = $('.owl-stage-outer').height();
//        $('.owl-carousel').css('width', owlItemWidth + 'px').css('height', owlStageOuterHeight + 'px').css('margin', '0');
//        $('.owl-stage').css('right', '2px');
//        $('.content').css('width', owlItemWidth + 'px');
        //        $('.foot').css('width', owlItemWidth + 'px');
        $('.owl-carousel').css('height', $('.owl-stage-outer').height() + 'px').css('margin', '0');
//        $('.owl-item').css('width', ($('.owl-item').width() + 4) + 'px');
        //        setResponsiveValue($('.owl-item'), [15], ['right']);

        $('.content').css('width', $('.owl-item div').width()  + 'px');
        $('.foot').css('width', $('.owl-item div').width() + 'px');

        setResponsiveValue($('.owl-dot span'), [7], ['width', 'height']);
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
        e.stopPropagation();
        e.cancelBubble = true;
    });
    $('.cover').on('touchstart', function(e) {
        hideLeftMenu();
        e.preventDefault();
        e.returnValue = false;
        e.stopPropagation();
        e.cancelBubble = true;
    });

    $('.left').on('touchmove', function (event) {
        var e = window.event || event;
        e.preventDefault();
        e.returnValue = false;
        e.stopPropagation();
        e.cancelBubble = true;
    });

    var startX, startY, moveEndX, moveEndY;
    $("div").on("touchstart", function (e) {
        startX = e.originalEvent.changedTouches[0].pageX,
        startY = e.originalEvent.changedTouches[0].pageY;
    });
    $("div").on("touchmove", function (e) {
        moveEndX = e.originalEvent.changedTouches[0].pageX;
        moveEndY = e.originalEvent.changedTouches[0].pageY;
        var X = moveEndX >= startX ? moveEndX - startX : startX - moveEndX;
        var Y = moveEndY >= startY ? moveEndY - startY : startY - moveEndY;

        if (X >= Y) {
            e.preventDefault();
            e.returnValue = false;
            e.stopPropagation();
            e.cancelBubble = true;
        }
        
    });
});


    
    

    