﻿var User = User || {};

(function () {

    User.SettingViewModel = function (theParams) {
        theParams = theParams || {};
        this.Socials = ko.observable([{ Name: "vk" }, { Name: "ok" }, { Name: "facebook" }].map(x => new Social.Model(x)));
        return this;
    };

    /**
    * Определяем конструктор
    */
    User.SettingViewModel.prototype.constructor = User.SettingViewModel;


})();