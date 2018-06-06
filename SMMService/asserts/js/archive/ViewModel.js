var Archive = Archive || {};

(function () {

    Archive.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        this.Posts = ko.observableArray(theParams.Posts ? theParams.Posts.map(function (item) { return new Post.PostModel(item) }) : []);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Archive.ViewModel.prototype.constructor = Archive.ViewModel;


})();