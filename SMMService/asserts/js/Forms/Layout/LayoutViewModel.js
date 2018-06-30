var Layout = Layout || {};

(function () {

    Layout.LayoutViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlGetProjects)
            console.error("Одна из ссылок не была переданна");
        this.UrlGetProjects = theParams.UrlGetProjects;

        var self = this;
        this.Projects = ko.observableArray(theParams.Projects ? theParams.Projects.map(function (item) { return new Project.ProjectModel(item) }) : []);
        this.GroupProjects = ko.computed(function () {
            return self.GroupBy(self.Projects(), "GroupName");
        });
        this.Search = ko.observable("");



        this.GetProjects();
        // Слушаем событие
        document.addEventListener('SetFavorite', function (e) {
            var project = self.Projects().filter(function (item) { return item.Id() == e.detail.Id })[0];
            project.IsFavorite(e.detail.IsFavorite);
        }, false)
        document.addEventListener('SetArhive', function (e) {
            var project = self.Projects().filter(function (item) { return item.Id() == e.detail.Id })[0];
            project.IsArhive(e.detail.IsArhive);
        }, false)
        document.addEventListener('SetGroup', function (e) {
            var project = self.Projects().filter(function (item) { return item.Id() == e.detail.ProjectId })[0];
            project.GroupName(e.detail.GroupName);
            project.GroupId(e.detail.GroupId);
        }, false)
        
        return this;
    };

    /**
    * Определяем конструктор
    */
    Layout.LayoutViewModel.prototype.constructor = Layout.LayoutViewModel;

    Layout.LayoutViewModel.prototype.GetProjects = function () {
        var self = this;
        $.post(this.UrlGetProjects, { search: this.Search()}).done(function (res) {
            self.Projects(res.map(function (item) { return new Project.ProjectModel(item) }));

        });
    }
    /**
     * Сортировать массив и выдать новый массив [{key:1,value:[]}]
     * @param {Массив который нужно сортировать} xs
     * @param {Ключ по которому будет происходить сортировка} key
     */
    Layout.LayoutViewModel.prototype.GroupBy = function (xs, key) {
        var properties = [];
        var group = xs.reduce(function (rv, x) {
            (rv[x[key]()] = rv[x[key]()] || []).push(x);
            return rv;
        }, {});
         ko.utils.objectForEach(group, function (key, value) {
            properties.push({ key: key, value: value });
        });
        return properties;
    }

    
})();

