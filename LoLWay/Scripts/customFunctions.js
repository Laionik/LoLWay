function listboxCheck() {
    // Check if the parent has reached the limit of selected items.
    if ($(this).parent().val().length > 6) {
        // Removed the selected attribute from this option.
        $(this).removeAttr("selected");
    }
};