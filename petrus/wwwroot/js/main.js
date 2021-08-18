$(".slider-one")
    .not(".slick-initialized")
    .slick({
        autoplay: true,
        dots: true,
        autoplaySpeed: 2000,
        prevArrow: '.prev',
        nextArrow: '.next'
    });