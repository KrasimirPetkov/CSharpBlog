function hideNav() {
    var didScroll;
    var lastScrollTop = 0;
    var delta = 5;
    var navbarHeight = $('nav').outerHeight();

    $(window).scroll(function (event) {
        didScroll = true;
    });

    setInterval(function () {
        if (didScroll) {
            hasScrolled();
            didScroll = false;
        }
    }, 250);

    function hasScrolled() {
        var st = $(this).scrollTop();

        if (Math.abs(lastScrollTop - st) <= delta)
            return;

        if (st > lastScrollTop && st > navbarHeight) {
            $('nav').addClass('nav-up', { duration: 1000 });
        } else {
            if (st + $(window).height() < $(document).height()) {
                $('nav').removeClass('nav-up', { duration: 1000 });
            }
        }
        lastScrollTop = st;
    }
}