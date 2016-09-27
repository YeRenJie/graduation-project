(function ($) {
    $.fn.textIntime = function (options) {
        var opt = $.extend({}, $.fn.textIntime.defaults, options);
        $.fn.limits(opt);
    };
    $.fn.textIntime.defaults = {
        maxLength: 200,
        divSize: "",
        divInput: ""
    };
    $.fn.limits = function (opts) {
        var counter = $(opts.divInput).val().length;
        $(opts.divSize).text(opts.maxLength - counter);
        $(document).keyup(function () {
            var text = $(opts.divInput).val();
            var counter = text.length;
            $(opts.divSize).text(opts.maxLength - counter);
            if (opts.maxLength - counter < 0) {
                $(opts.divSize).text("0");
                $(opts.divInput).val($(opts.divInput).val().substr(0, 300));
            }
        });
    };
})(jQuery)