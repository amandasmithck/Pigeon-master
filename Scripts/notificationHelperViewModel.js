function notificationHelperViewModel () {

    notify = function(msg, isSuccess, title)
    {
        //window.alert(msg);
        //return false;
        if ((isSuccess == true || isSuccess == false) == false) { window.alert(err); return false; }
        //all good
        swal({ title: (title != null && title != "undefined" && title.length > 0 ? title : isSuccess == true ? "Success" : "Problem"), html: msg, type: isSuccess == true ? "success" : "error", timer: 5000, showConfirmButton: true, closeOnConfirm: true, animation: "slide-from-top" });
    };

    inform = function (msg) {       
        swal({ title: "Notice!", text: msg, type: "info", timer: 5000, showConfirmButton: true, closeOnConfirm: true, animation: "slide-from-top" });
    };

    wait = function()
    {
        swal({ title: "One Moment. . . .", animation: "slide-from-top", html: "<h5>Loading . . . .</h5>", imageUrl: "/images/ajax-loader.gif", showConfirmButton: false, showCancelButton: false});
    }

    killAlert = function ()
    {
        swal({ text: "", timer: -1, showConfirmButton: false }); // swal("");
    }

    return{
        notify : notify,
        wait : wait,
        killAlert: killAlert,
        inform:inform
    }
};

