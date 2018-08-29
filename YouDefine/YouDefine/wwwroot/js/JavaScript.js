//(function () {

//})();

function loadBackgroundColor() {
    $("body").animate({ backgroundColor: "red" }, "slow");
    console.log("chuj kurwa");
}

//(function () {
//    loadBackgroundColor();
//    $("#search-input").click(function () {

//    }


//})();
 
$("#search-input").click(function () {
    $("body").addClass('yellow');
});

//$.ajax({
//    url: "/api/getWeather",
//    data: {
//        zipcode: 97201
//    },
//    success: function (result) {
//        $("#weather-temp").html("<strong>" + result + "</strong> degrees");
//    }
//});