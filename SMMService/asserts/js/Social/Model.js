var Social = Social || {};

(function () {

    Social.Model = function (theParams) {
        theParams = theParams || {};
        this.IsActive = ko.observable(theParams.IsActive || false);
        this.Name = ko.observable(theParams.Name || "");
        this.Groups = ko.observableArray(theParams.Groups || []);
        this.IsVisibleGroup = ko.observable(false);
        if (theParams.Group)
            theParams.Group = this.SetGroup(theParams.Group);
        this.SelectedGroup = ko.observable(theParams.Group || null);
        this.IsAuth = ko.observable(theParams.IsAuth || false);
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
    Social.Model.prototype.SetGroup = function (data) {
        if (!data.Id)
            return undefined;
        var group = this.Groups().filter(function (item) { return item.Id == data.Id })[0];
        if (group)
            this.IsActive(true);
        return group;
    }

   
})();