var Social = Social || {};

(function () {

    Social.Model = function (theParams) {
        theParams = theParams || {};
        this.IsActive = ko.observable(theParams.IsActive || false);
        this.Name = ko.observable(theParams.Name || "");
        this.Groups = ko.observableArray(theParams.Groups || []);
        this.IsVisibleGroup = ko.observable(false);
        this.SelectedGroup = ko.observable(theParams.Group || null);      
        return this;
    };

    /**
    * Определяем конструктор
    */
    Social.Model.prototype.constructor = Social.Model;

    Social.Model.prototype.SelectGroup = function (group) {
        this.SelectedGroup(group);
        this.IsVisibleGroup(false);
    }
    Social.Model.prototype.GetData = function () {
        switch (this.Name()) {
            case "ok":
                return {
                    GroupOK: this.SelectedGroup().Id
                }
                break;
            case "vk":
                return {
                    GroupVk: this.SelectedGroup().Id
                }
                break;           
        }     
    }

})();