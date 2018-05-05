var Social = Social || {};

(function () {

    Social.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.Socials = ko.observable(theParams.Socials ? theParams.Socials.map(function (x) { return new Social.Model(x) }) : []);     
        return this;
    };

    /**
    * Определяем конструктор
    */
    Social.ViewModel.prototype.constructor = Social.ViewModel;

    Social.ViewModel.prototype.OffModal = function (social) {
        this.Socials().forEach(function (item) {
            item.IsVisibleGroup(false);
        });
        if (social) {
            social.IsVisibleGroup(true);
        }
    }

})();