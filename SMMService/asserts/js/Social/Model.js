var Social = Social || {};

(function () {

    Social.Model = function (theParams) {
        theParams = theParams || {};
        this.IsActive = ko.observable(theParams.IsActive || false);
        this.Name = ko.observable(theParams.Name || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Social.Model.prototype.constructor = Social.Model;


})();