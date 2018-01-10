var Module = (function (container, options) {
    var pub = {};

    var defaults = {
        
    };
    options = mergeObject(defaults, options);

    /*begin module logic*/



    /*end module logic*/

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
    
    /* public functions to be accessible from the outside */
    pub.render = function () {

    };

    return pub;
});