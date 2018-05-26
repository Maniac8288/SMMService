ko.bindingHandlers.iconName  = {
    init: function (element, valueAccessor, allBindings) {
        var name = valueAccessor();
        element.className += " icon-"+name();
    }
}
