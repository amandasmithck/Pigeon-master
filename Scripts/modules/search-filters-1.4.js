var SearchFilters = (function (container, options, user, returnFunction) {
    var pub = {},
        $c = $(container);

    var defaults = {
        css: {}
        , filters: ['Year', 'Make', 'Model']
        , growable: true
        , asmxLocation: '../PigeonWebService.asmx/'
        , initMethod: 'GetData'
        , nextMethod: 'GetData'
        , resultsMethod: 'GetData'
        , debug: false
        , tooltip: true
        , preResultsFunc: function () { }
    };
    options = mergeObject(defaults, options);

    var __init = function () {
        __populate(0);

        if (options.tooltip) {
            $c.popover({
                title: "Start your search here",
                content: "Fill in all search filters to locate a part match.",
                placement: 'right',
                trigger: 'manual'
            });

            setTimeout(function () { $c.popover('show'); }, 1500);

            $('body').click(function () { $c.popover('hide'); });
            $c.hover(function () { $c.popover('hide'); });
        }
    },

    __populate = function (i) {
        var index = i;

        //fire the pre-results function to add functionality or display (loading message) before the results are returned
        if (index === options.filters.length) options.preResultsFunc();

        //reset all subsequent selects to make sure no residual data is sent over from past filtering
        for (j = index; j <= options.filters.length; j++) {
            $('.sidebar-search-form select:nth(' + (j) + ')').empty().append("<option></option>");
        }

        //use the correct web service depending on which drop down is being populated
        switch (i) {
            case 0:
                var urlMethod = options.asmxLocation + options.initMethod;
                break;
            case options.filters.length:
                var urlMethod = options.asmxLocation + options.resultsMethod;
                break;
            default:
                var urlMethod = options.asmxLocation + options.nextMethod;
        }
        var data = {
            'source': user.Role.charAt(0).toUpperCase() + user.Role.slice(1) //capitalizing the role (ie customer -> Customer) this is inconsistent across the platform
            , 'name': user.UserName
            , 'client': user.Client
            , 'customerNo': $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
            , 'customerEmail': $('#CustomerEmailLabel').text() != "" ? $('#CustomerEmailLabel').text() : user.UserName
            , 'customerClient': $('#PigeonCompanies').css('display') == "block" ? $('#CustomerNameLabel').attr('relClient') : user.Client
            , 'tierID': user.Role=='admin' ? $('#txtEmail').val() == "" ? $('#CustomerNameLabel').attr('reltierID') : $('#FrontDDLEmail :selected').attr('reltierID') : user.Tier

        };
        //this is a pretty hacked togther data payload for the webservice, the string "null" means there is nothing selected
        $(options.filters).each(function (i, x) {
            var filter = x.toLowerCase();
            data[filter] = ($('.' + filter + ' option:selected').val() == "") ? "null" : $('.' + filter + ' option:selected').val();
        });

        var jsonData = JSON.stringify(data);

        __sendAjax(urlMethod, jsonData, function (msg) {
            response = jQuery.parseJSON(msg.d);

            //if (window.console && options.debug) console.log(msg.d);

            /*
            * Smart dropdowns
            */

            //if this is the last dropdown the axaj response will be a search result set and should be handled by the return func
            if (index === options.filters.length && $('.sidebar-search-form select:last').val() != "") {
                returnFunction(response);
                return true;
            }

            $(response).each(function () {
                $('.sidebar-search-form select:nth(' + (index) + ')').append("<option>" + this.thevalue + "</option>");
            });

            if (options.growable) {
                $('.sidebar-search-form select:eq(' + (index) + ')').each(function () {
                    var $this = $(this);

                    var width = $this.outerWidth();

                    $this.data("origWidth", width);

                    $this
                    .mousedown(function () {
                        if ($this.css("width") != "auto") {
                            $this.css("width", "auto");

                            if ($this.width() < width) {
                                $(this).unbind('mousedown');
                                $(this).css("width", $(this).data("origWidth"));
                            }
                        }
                    })
                    .blur(function () {
                        $this.css("width", $(this).data("origWidth"));
                    })
                    .change(function () {
                        $this.css("width", $(this).data("origWidth"));
                    });
                });
            }

            //hide and only show dropdowns up to the one that is being changed
            $('.sidebar-search-form select, .sidebar-search-form label').hide();
            for (n = 0; n <= index; n++) {
                $('.sidebar-search-form select:nth(' + n + ')').prev('label').show().end().show();
            }

            //if (window.console && options.debug) console.log(index + " of " + options.filters.length);

            //if the response only has one option to choose from go ahead and select that for the user and go to the next
            if (response.length == 1) {
                $('.sidebar-search-form select:eq(' + (index) + ') option:nth(1)').prop('selected', true)
                __populate(++index, urlMethod);
            }
        });
    },

    __sendAjax = function (urlMethod, jsonData, returnFunction) {
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: urlMethod,
            data: jsonData,
            dataType: "json",
            success: function (msg) {
                if (msg != null) {
                    returnFunction(msg);
                }
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                //alert(err.Message);
            }
        });
    };

    /**
    * merge 2 objects into a new object
    * @param   object  obj1
    * @param   object  obj2
    * @return  object  merged object
    */
    function mergeObject(obj1, obj2) {
        var output = {};

        if (!obj2) {
            return obj1;
        }

        for (var prop in obj1) {
            if (prop in obj2) {
                output[prop] = obj2[prop];
            } else {
                output[prop] = obj1[prop];
            }
        }
        return output;
    }

    pub.render = function () {
        $c.empty(); //just in case render is called twice on the same container ;)

        $c.addClass('sidebar-search-form').css(options.css);

        $(options.filters).each(function (i, x) {
            $c.append("<label style='display:none;'>" + x + "</label><select style='display:none;' class='" + x.toLowerCase() + "'><option></option></select>");
        });

        //register change events for each select
        $c.find('select').change(function () {
            var i = $('.sidebar-search-form select').index(this);
            __populate(++i);
        });

        //initilize the drop down set (fill in values to initial drop down)
        __init();

    };

    return pub;
});