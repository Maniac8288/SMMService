ko.bindingHandlers.emoji  = {
    init: function (element, valueAccessor, allBindings) {
        var value = valueAccessor();
        $(element).emojioneArea({
            pickerPosition: "top",
            tonesStyle: "bullet",
            filtersPosition: "bottom",
            //hidePickerOnBlur: false,
            search: false,
            tones: false
        });
    }
}
