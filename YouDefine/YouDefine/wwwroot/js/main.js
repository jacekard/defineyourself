//(function () {

//})();

//$.ajax({
//    url: "/api/getWeather",
//    data: {
//        zipcode: 97201
//    },
//    success: function (result) {
//        $("#weather-temp").html("<strong>" + result + "</strong> degrees");
//    }
//});


(function () {
    console.log("dzialam");


    $("#search-input").click(function () {
        var option = String(Math.floor((Math.random() * 3) + 1));
        var className = "background-variation" + option
        $("body").removeClass();
        $("body").addClass(className);
        $("#tooltip").removeClass();
        $("#tooltip").addClass("tooltip-active");
    });


    $(document).keypress(function (e) {
        if (e.which == 13) {
            //enter key pressed
        }
    });
})();