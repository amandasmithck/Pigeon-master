$('document').ready(function () {
    $('.btn-tools').click(function (e) {
        e.preventDefault();

        var el = $('.top-panel');

        if (!el.hasClass('open')) {
           $('.btn-tools').css({ 'background-position': 'center bottom' });

            curHeight = el.height(),
            autoHeight = el.css('height', 'auto').height();
            minHeight = 140;
            el.height(curHeight).animate({ height: (minHeight > autoHeight) ? minHeight : autoHeight }, 200);

            el.toggleClass('open');
        } else {
            $('.btn-tools').css({ 'background-position': 'center top' });
            el.animate({ height: 0 }, 200);
            el.toggleClass('open');
        }
    });
});