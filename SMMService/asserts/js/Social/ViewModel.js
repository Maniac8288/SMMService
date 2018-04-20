var Social = Social || {};

(function () {

    Social.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.Socials = ko.observable([{ Name: "vk" }, { Name: "ok" }, { Name: "facebook" }].map(x => new Social.Model(x)));
        return this;
    };

    /**
    * Определяем конструктор
    */
    Social.ViewModel.prototype.constructor = Social.ViewModel;


})();