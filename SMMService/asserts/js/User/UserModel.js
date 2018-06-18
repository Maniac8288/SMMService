var User = User || {};

(function () {

    User.UserModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.FirstName = ko.observable(theParams.FirstName || "");
        this.LastName = ko.observable(theParams.LastName || "");
        this.ImageUrl = ko.observable(theParams.ImageUrl || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    User.UserModel.prototype.constructor = User.UserModel;

  
})();