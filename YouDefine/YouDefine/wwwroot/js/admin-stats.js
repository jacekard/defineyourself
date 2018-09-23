var uri = '../../api/stats/';

function websiteInfo() {
    var url = uri + 'websiteInfo';

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            console.log(data);
            var msg = "YouDefine website is already running "
                + data.days + " day(s), " + data.hours + " hour(s), "
                + data.minutes + " minute(s) and " + data.seconds + " seconds.";
            $("#website-info").text(msg);
        }
    });
}

function characterCountStats() {
    var url = uri + 'charsCount';

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            var msg = "In total, there are " + data.ideasChars.toString()
                + " characters in all ideas titles and "
                + data.definitionsChars.toString() + " characters in definitions.";
            $("#character-stats").text(msg);
        }
    });
}

(function () {
    "use strict";

    $(".stats-page").addClass("active");
    websiteInfo();
    characterCountStats();
})();