var Project = Project || {};

(function () {

    Project.ProjectViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlSave)
            console.error("Одна из ссылок не была переданна");
        this.UrlSave = theParams.UrlSave;
        this.Project = new Project.ProjectModel(theParams.Project);
        this.Socials = theParams.Socials;
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectViewModel.prototype.constructor = Project.ProjectViewModel;

    Project.ProjectViewModel.prototype.fileUpload = function (data, e) {
        var self = this;
        this.Project.ImageFile(e.target.files[0]);     
    };
    Project.ProjectViewModel.prototype.Edit = function () {
        var self = this;
        var model = this.Project.GetData();
        var socials = this.Socials().filter(function (item) {
            return item.IsActive();
        }).map(function (item) { return item.GetData(); });
        

        var formData = new FormData();    
        formData.append("Name", model.Name);
        formData.append("Id", model.Id);
        formData.append("ImageFile", model.ImageFile);
        socials.forEach(function (item) {
            for (key in item) {
                formData.append(key, item[key]);
            }
        })
       
        $.ajax({
            url: this.UrlSave,
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (res) {
                console.log(res);
            }
        });
    }
})();