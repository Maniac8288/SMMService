var Project = Project || {};

(function () {

    Project.ProjectViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlSave)
            console.error("Одна из ссылок не была переданна");
        this.UrlSave = theParams.UrlSave;
        this.Project = new Project.ProjectModel(theParams.Project);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectViewModel.prototype.constructor = Project.ProjectViewModel;

    Project.ProjectViewModel.prototype.fileUpload = function (data, e) {
        var self = this;
        this.Project.ImageFile(e.target.files[0]);
        //var reader = new FileReader();

        //reader.onloadend = function (onloadend_e) {
        //    var result = reader.result; // Here is your base 64 encoded file. Do with it what you want.
        //    self.Project.ImageFile(result);
        //};

        //if (file) {
        //    reader.readAsDataURL(file);
        //}
    };
    Project.ProjectViewModel.prototype.Edit = function () {
        var self = this;
        var model = this.Project.GetData();
        var formData = new FormData();    
        formData.append("Name", model.Name);
        formData.append("Id", model.Id);
        formData.append("ImageFile", model.ImageFile);
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