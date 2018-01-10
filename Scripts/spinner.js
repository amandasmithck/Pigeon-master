var spinnerOpts = {
    lines: 13 // The number of lines to draw
            , length: 20 // The length of each line
            , width: 10 // The line thickness
            , radius: 40 // The radius of the inner circle
            , scale: 1 // Scales overall size of the spinner
            , corners: 0.9 // Corner roundness (0..1)
            , color: '#F88017' // #rgb or #rrggbb or array of colors
            , opacity: 0.40 // Opacity of the lines
            , rotate: 1 // The rotation offset
            , direction: 1 // 1: clockwise, -1: counterclockwise
            , speed: 3 // Rounds per second
            , trail: 60 // Afterglow percentage
            , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
            , zIndex: 2e9 // The z-index (defaults to 2000000000)
            , className: 'spinner' // The CSS class to assign to the spinner
            , top: '51%' // Top position relative to parent
            , left: '50%' // Left position relative to parent
            , shadow: false // Whether to render a shadow
            , hwaccel: false // Whether to use hardware acceleration
            , position: 'absolute' // Element positioning
}
var spinner;
