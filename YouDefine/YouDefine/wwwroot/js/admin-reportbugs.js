var uri = "../../api/bugs/";

function updateProgressBar() {
    var url = uri + 'progress';
    var successMsg = " reports (completed)";
    var warningMsg = " reports (undone)";

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (data.success === -1) {
                $(".progress-bar-success").text("No bug reports");
                $(".progress-bar-success").css("width", "100%");
                $(".progress-bar-warning").text("");
                $(".progress-bar-warning").css("width", "0%");
            } else {
                var success = data.success + "%";
                var failure = data.failure + "%";
                $(".progress-bar-success").text(success + successMsg);
                $(".progress-bar-success").css("width", success);
                $(".progress-bar-warning").text(failure + warningMsg);
                $(".progress-bar-warning").css("width", failure);
            }
        }
    });
}

function sortByDropdown() {
    //sort method is to be implemented!
    $(".sort-option").text("newest");
    $(".sort-option").text("oldest");
}

function changeBugStatus(row, status, bugId) {
    var url = uri + status + '/' + bugId;

    $.ajax({
        type: 'PUT',
        url: url,
        success: function (data) {
            console.log("ok");
        }
    });

    row.remove();
}

function getActiveReports() {
    var url = uri + 'active';

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            var selector = "#table-body-active";
            updateTables(data, selector, false, 'ok');
        }
    });
}

function getCompletedReports() {
    var url = uri + 'completed';

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            var selector = "#table-body-completed";
            updateTables(data, selector, false, 'no');
        }
    });
}

function getAllReports() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            var selector = "#table-body-all";
            updateTables(data, selector, true, 'null');
        }
    });
}


//statusColor: true = color rows / false = don't color rows
//bugStatusOption: 'ok' = isFixed{true} / 'no' = isFixed{false} / 'null' = no action
function updateTables(data, selector, statusColor, bugStatusOption) {

    $(selector).empty();

    $.each(data, function (index, bug) {
        var rowId = bug.id;
        var row;
        if (statusColor) {
            if (bug.isFixed) {
                row = jQuery('<tr/>', {
                    class: bug.id + ' success'
                }).appendTo(selector);
            } else {
                row = jQuery('<tr/>', {
                    class: bug.id + ' danger'
                }).appendTo(selector);
            }
        } else {
            row = jQuery('<tr/>', {
                class: bug.id
            }).appendTo(selector);
        }

        jQuery('<td/>', {
            text: bug.id
        }).appendTo('.' + bug.id);

        jQuery('<td/>', {
            text: bug.information
        }).appendTo('.' + bug.id);

        jQuery('<td/>', {
            text: bug.reportDate.substr(0, 10)
        }).appendTo('.' + bug.id);

        if (bugStatusOption !== 'null') {
            var actionTd = jQuery('<td/>', {
            }).appendTo('.' + bug.id);

            var buttonText
            if (bugStatusOption === 'no') {
                buttonText = "Not Completed"
            } else {
                buttonText = "Completed";
            }

            var button = jQuery('<button/>', {
                type: 'submit',
                id: 'button' + bug.id,
                class: 'btn btn-default',
                text: buttonText
            }).appendTo(actionTd);

            button.on('click', function () {
                changeBugStatus(row, bugStatusOption, bug.id);
            });
        }
    })
}


(function () {
    "use strict";
    console.log("dzialam");
    $(".bugreports-page").addClass("active");

    updateProgressBar();
    sortByDropdown();
    getActiveReports();
    getCompletedReports();
    getAllReports();

    $("#active-tab").click(function () {
        getActiveReports();
    });

    $("#completed-tab").click(function () {
        getCompletedReports();
    });

    $("#all-tab").click(function () {
        getAllReports();
    });
})();