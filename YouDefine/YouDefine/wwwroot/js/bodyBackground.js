(function () {
    var option = String(Math.floor((Math.random() * 4) + 1));
    var className = "background-variation" + option
    $("body").removeClass();
    $("body").addClass(className);
})();