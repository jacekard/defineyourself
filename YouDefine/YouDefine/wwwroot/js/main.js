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
        console.log(className);
        $("body").removeClass();
        $("body").addClass(className);
    });
})();