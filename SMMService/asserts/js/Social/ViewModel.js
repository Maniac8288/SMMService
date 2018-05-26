var Social = Social || {};

(function () {

    Social.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlAuthorizationOk = theParams.UrlAuthorizationOk;
        this.UrlAuthorizationVk = theParams.UrlAuthorizationVk;
        this.Socials = ko.observable(theParams.Socials ? theParams.Socials.map(function (x) { return new Social.Model(x) }) : []);
        this.SelectSocial = ko.observable(new Social.Model());
        return this;
    };

    /**
    * Определяем конструктор
    */
    Social.ViewModel.prototype.constructor = Social.ViewModel;

    Social.ViewModel.prototype.OffModal = function (social) {
        if (!social.IsAuth()) {
            $("#notAuth").modal("show");
            this.SelectSocial(social);
            return;
        }
        this.Socials().forEach(function (item) {
            item.IsVisibleGroup(false);
        });
        if (social) {
            social.IsVisibleGroup(true);
        }
    }
    Social.ViewModel.prototype.Authorization = function (social) {
        var url = "";
        switch (social.Name()) {
            case "ok":
                return  this.UrlAuthorizationOk;
                break;
            case "vk":
                return this.UrlAuthorizationVk;
                break;
            default:
                console.error("Социальная сеть не найдена");
                return "";
        }       
    }

})();