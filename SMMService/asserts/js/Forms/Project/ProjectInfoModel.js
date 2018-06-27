var Project = Project || {};

(function () {


    Project.ProjectInfoModel = function (theParams) {
        theParams = theParams || {};
        theParams.JsonCards = JSON.parse(theParams.JsonCards);
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.MainInfo = ko.observable(theParams.MainInfo || "");
        this.AdditionalInfo = ko.observable(theParams.AdditionalInfo || "");
        this.JsonCards = ko.observableArray(theParams.JsonCards ? theParams.JsonCards.map(function (item) { return new Project.ProjectCardsModel(item) }) : []);
        this.Card = ko.observable(new Project.ProjectCardsModel());
        this.Hashtags = ko.observableArray(theParams.Hashtags ? theParams.Hashtags.map(function (item) { return new Project.HashTagModel(item) }) : []);
        this.HashtagTitle = ko.observable("#");
        this.EditAdditionalInfo = ko.observable(false);
        this.EditMainInfo = ko.observable(false);

        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectInfoModel.prototype.constructor = Project.ProjectInfoModel;

    /**
     * Получить данные 
     */
    Project.ProjectInfoModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Name: this.Name(),
            MainInfo: this.MainInfo(),
            AdditionalInfo: this.AdditionalInfo(),
            JsonCards: JSON.stringify(this.JsonCards().filter(function (x) { return x.Info() != "" }).map(function (item) { return item.GetData() })),
            Hashtags: this.Hashtags().map(function (item) { return item.GetData() })
        }
    }
    /**
     * Добавить карточку 
     */
    Project.ProjectInfoModel.prototype.AddCard = function () {
        if (this.Card().Info() == "" || this.Card().Name() == "") {
            this.Card().Error("Заполните все поля");
            return;
        }
        this.JsonCards.push(this.Card());
        this.Card(new Project.ProjectCardsModel());
    }
    /**
     * Добавить хэштэг 
     **/
    Project.ProjectInfoModel.prototype.AddHashTag = function () {
        var hashTag = {
            Title: this.HashtagTitle(),
            ProjectId: this.Id()
        }
        this.Hashtags.push(new Project.HashTagModel(hashTag));
        this.HashtagTitle("#");
    }
})();