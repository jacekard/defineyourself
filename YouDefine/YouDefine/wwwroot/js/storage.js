'use strict';

function Storage() { }

Storage.prototype.get = function (key) {
    return window.localStorage.getItem(key);
};

Storage.prototype.set = function (key, value) {
    window.localStorage.setItem(key, value);
};

Storage.prototype.clear = function () {
    window.localStorage.clear();
};

Storage.prototype.print = function (key) {
    console.log(JSON.parse(this.get(key)));
};

Storage.prototype.init = function () {
    if (!this.get('ldefs')) {
        this.set('ldefs', 0);
    }
};