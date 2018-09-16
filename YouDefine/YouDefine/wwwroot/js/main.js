//(function () {

//})();

var uri = 'api/ideas/';
var ideas = new Array();
var messages = [
    "you can define it!",
    "maybe it's your turn?",
    "what do you have in mind?",
    "tell us what you think.",
    "is it even a word?",
    "define it, please",
    "press + to add it.",
    "add this to our dict"
];
var storage = new Storage();
storage.init();
var isSearchAgain = false;
var ideaMatched = false;
var currentIdea;

function validateDefinitionsLS(id) {
    var str = storage.get('ldefs');
    var list = str.split(',');
    var result = true;
    list.forEach(function (i) {
        if (i == id) {
            result = false;
        }
    });

    return result;
}

function createDefinitions(idea, def, prepend) {
    var divId = 'def' + def.id;
    var infoId = 'span' + def.id;
    var iconId = 'thumb-up' + def.id;
    var likesId = 'likes' + def.id;

    var div = jQuery('<div/>', {
        class: 'single-definition ' + divId
    });

    if (prepend === true) {
        div.prependTo("#definition-container");
    } else {
        div.appendTo("#definition-container");
    }

    jQuery('<div/>', {
        class: 'definition-info ' + infoId
    }).appendTo('.' + divId);

    var likesCount = jQuery('<span/>', {
        class: 'definition-likes-count ' + likesId,
        text: def.likes
    }).appendTo('.' + infoId);

    var icon = jQuery('<i/>', {
        class: 'material-icons definition-likes-icon ' + iconId,
        text: 'thumb_up'
    }).prependTo('.' + infoId);

    if (validateDefinitionsLS(def.id)) {
        icon.on('click', function () {
            likeDefinition(idea, def, likesCount, this);
        });
    } else {
        icon.on('click', function () {
            unlikeDefinition(idea, def, likesCount, this);
        });
        colorIconLikesAndCount(true, likesCount, icon);
    }

    jQuery('<div/>', {
        class: 'definition-date',
        text: def.date
    }).appendTo('.' + divId);

    jQuery('<div/>', {
        class: 'definition-text',
        text: def.text
    }).appendTo('.' + divId);

    $('.' + divId).toggleClass("slideToLeft-animation");
}

//when was found
function specifiedIdea(idea) {
    isSearchAgain = true;
    $("#search-input").autocomplete("disable");
    $("#definition-container").empty();

    var title = idea.title;
    var likes = idea.likes;
    var date = idea.lastModifiedDate;

    idea.definitions.forEach(function (def) {
        createDefinitions(idea, def, false);
    });

    //probably to be removed
    $("#search-input").autocomplete("search", "");
    //
    if ($(".search-container").hasClass("search-input-active")) {
        $(".search-container").removeClass("search-input-active");
    }
    $(".search-container").addClass("search-input-hidden");
    $("#idea-likes-count").text(likes);
    $(".idea-date").text(date);
    $(".idea-date").addClass("show-element");
    $(".idea-info").addClass("show-element");
    $(".add-new-panel").removeClass("show-element");
    $(".put-definition").addClass("show-element");
}


function searchAgain() {
    //deletes div, should be animation and then empty();
    $("#definition-container").empty();
    $("#search-input").autocomplete("enable");

    $(".idea-info").removeClass("show-element");
    $(".idea-date").removeClass("show-element");
    $(".search-container").removeClass("search-input-hidden");
    $(".search-container").addClass("search-input-active");
    $(".put-definition").removeClass("show-element");
}

//when wasn't found
function showNewIdeaPanel(title) {
    if (isSearchAgain)
        return;
    var message = messages[Math.floor(Math.random() * messages.length)];
    var titleMsg = "We haven't found";
    $(".add-new-panel").addClass("show-element");
    $("#new-idea-title").text(titleMsg);
    $("#new-idea-message").text(message);
    $("#new-idea").html(title + ", ");
    $("#new-idea").fadeOut();
    $("#new-idea").fadeIn();

    //deletes div, should be animation and then empty();
    $("#definition-container").empty();
}

function newIdeaSuccessfullyPosted(showDefs) {
    $(".add-idea-button").fadeOut();
    $("#definition-input").fadeOut();
    $("#definition-input").css("display", "none");
    $(".add-idea-button").css("display", "none");
    $(".success-message").addClass("message-animation");
    setTimeout(function () {
        $(".success-message").removeClass("message-animation");
    }, 4000);

    if (showDefs === true) {
        specifiedIdea(currentIdea);
    }
}

function postNewIdea(title, text) {
    $.ajax({
        type: 'POST',
        url: uri,
        data: {
            'title': title,
            'text': text
        },
        success: function (data) {
            currentIdea = data;
            getIdeas();
            newIdeaSuccessfullyPosted(true);
        },
        fail: function (data) {
            $(".failure-message").addClass("message-animation");
        }
    });
}

function putNewIdea(title, text) {
    $.ajax({
        type: 'PUT',
        url: uri,
        data: {
            'title': title,
            'text': text
        },
        success: function (data) {
            createDefinitions(currentIdea, data, true);
            newIdeaSuccessfullyPosted(false);
        },
        fail: function (data) {
            $(".failure-message").addClass("message-animation");
        }
    });
}

function likeDefinition(idea, def, likesCount, icon) {
    var url = uri + "like";
    $.ajax({
        type: 'PUT',
        url: url,
        data: {
            'title': idea.title,
            'id': def.id
        },
        success: function (data) {
            $(likesCount).text(data.defLikes);
            $("#idea-likes-count").text(data.ideaLikes);
            $(icon).off();
            $(icon).on('click', function () {
                unlikeDefinition(idea, def, likesCount, icon);
            });
            colorIconLikesAndCount(true, likesCount, icon);
            var str = storage.get('ldefs');
            if (!str) {
                storage.set('ldefs', def.id);
            } else {
                var list = str.split(',');
                list.push(def.id);
                storage.set('ldefs', list.toString());
            }
        }
    });
}

function unlikeDefinition(idea, def, likesCount, icon) {
    var url = uri + "unlike";
    $.ajax({
        type: 'PUT',
        url: url,
        data: {
            'title': idea.title,
            'id': def.id
        },
        success: function (data) {
            $(likesCount).text(data.defLikes);
            $("#idea-likes-count").text(data.ideaLikes);
            $(icon).off();
            $(icon).on('click', function () {
                likeDefinition(idea, def, likesCount, icon);
            });
            colorIconLikesAndCount(false, likesCount, icon);
            var str = storage.get('ldefs');
            if (str) {
                var list = str.split(',');
                var index = list.findIndex(function (i) {
                    return i == def.id;
                });
                list.splice(index, 1);
                storage.set('ldefs', list.toString());
            }
        }
    });
}

//  true is green
//  false is initial
function colorIconLikesAndCount(option, likesCount, icon) {
    if (option) {
        $(likesCount).css('color', '#008000');
        $(icon).css('color', '#008000');
    } else {
        $(likesCount).css('color', '#778899');
        $(icon).css('color', '#778899');
    }
}

function getSpecifiedIdea(title) {
    if (title.length < 2) {
        return;
    }

    $.ajax({
        type: 'GET',
        url: uri,
        data: {
            'title': title
        },
        statusCode: {
            204: function () {
                ideaMatched = false;
                showNewIdeaPanel(title);
            },
            200: function (data) {
                console.log(data);
                ideaMatched = true;
                currentIdea = data;
                specifiedIdea(data);
            }
        },
        complete: function (xhr, status) {
        }
    });
}

function autocomplete() {
    if (ideas.length === 0) {
        setTimeout(autocomplete, 1);
        return;
    }

    $("#search-input").autocomplete({
        source: ideas,
        minLength: 1,
        classes: {
            "ui-autocomplete": "ui-autocomplete"
        },
        appendTo: "#result-container",
        delay: 1000,
        autoFocus: true,
        select: function (event, ui) {
            getSpecifiedIdea(ui.item.value);
        },
        search: function (event, ui) {
            getSpecifiedIdea($("#search-input").val());
        },
        messages: {
            noResults: '',
            results: function () { }
        }
    });
}

function assignData(data) {
    ideas = data;
}

function getIdeas() {
    $.ajax({
        type: 'GET',
        url: uri + 'titles',
        success: function (data) {
            assignData(data);
        }
    });
}

function getRandomIdea() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            var rand = Math.floor(Math.random() * data.length);
            if (!data[rand]) {
                return;
            }
            $("#search-input").val(data[rand]);
            getSpecifiedIdea(data[rand]);
        },
        fail: function (data) {
            console.log("fail!");
        }
    });
}

function addNewElementVisible() {
    $(".add-idea-button").fadeIn();
    $("#definition-input").fadeIn();
    $("#definition-input").css("display", "inline-block");
    $("#definition-input").val("");
    $(".add-idea-button").css("display", "inline-block");
    $(".add-new-panel").removeClass("show-element");

    if (isSearchAgain) {
        $(".add-idea-button").val("add definition");
    } else {
        $(".add-idea-button").val("add idea");
    }
}

function welcomeMessage() {
    var isFirstTime = storage.get('visited');
    if (!isFirstTime) {
        $(".welcome-message").addClass('welcome-message-animation');

        setTimeout(function () {
            $(".welcome-message").remove();
        }, 10000);
        storage.set('visited', true);
    }
}

(function () {
    "use strict";
    console.log("dzialam");

    welcomeMessage();
    getIdeas();
    autocomplete();

    $("#search-input").on('change keyup paste', function () {
        var val = $(this).val();
        $(this).val(val.toLowerCase());

        if (val.length < 3) {
            $("#definition-input").css("display", "none");
            $(".add-idea-button").css("display", "none");
            $(".add-new-panel").removeClass("show-element");
        }

        // ! ! ! ! NOT DONE YET (NEEDS A BETTER BOOLEAN VARIABLE)
        // must define a state of being "on top" and then disable add-new-panel display
        //if (ideaMatched) {

        //}
        // ! ! ! !

        if (val === "" && isSearchAgain) {
            isSearchAgain = false;
            searchAgain();
        }
    });

    $("#description-button").click(function () {
        var panel = $("#description-panel");

        if (panel.hasClass("tooltip-inactive")) {
            panel.removeClass();
            panel.addClass("tooltip-open");
        } else {
            panel.removeClass();
            panel.addClass("tooltip-close");
            panel.delay(1500).queue(function (next) {
                panel.addClass("tooltip-inactive");
                next();
            });
        }

        $(this).toggleClass("button-spin");
    });

    $("#description-button").hover(function () {
        $(this).toggleClass("button-spin");
    });

    $("#refresh-button").click(function () {
        getRandomIdea();
    });

    $(".add-new-icon").click(function () {
        addNewElementVisible();
    });

    $(".put-definition").click(function () {
        addNewElementVisible();
    });

    $(".add-idea-button").click(function () {
        var title = $("#search-input").val();
        var text = $("#definition-input").val();
        if (title !== "" && text !== "") {
            if ($(".add-idea-button").val() === "add idea") {
                postNewIdea(title, text);
            } else {
                putNewIdea(title, text);
            }
        }
    });

    //$(document).keypress(function (e) {
    //    if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
    //        getRandomIdea();
    //    }
    //});

    setInterval(getIdeas, 5000);
})();