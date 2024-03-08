var textSeparator = ", ";
function OnListBoxSelectionChanged(listBox, args, dropDown) {
    UpdateSelectAllItemState(listBox);
    UpdateText(listBox, dropDown);
}
function CheckAll(s, e, listBox, dropDown) {
    if (s.GetText() == "Check All") {
        listBox.SelectAll();
        s.SetText("Uncheck All");
    }
    else {
        listBox.UnselectAll();
        s.SetText("Check All");
    }
    UpdateSelectAllItemState(listBox);
    UpdateText(listBox, dropDown);

}
function UpdateSelectAllItemState(listBox) {

}
function IsAllSelected(listBox) {
    var selectedDataItemCount = listBox.GetItemCount() - (listBox.GetItem(0).selected ? 0 : 1);
    return listBox.GetSelectedItems().length == selectedDataItemCount;
}
function UpdateText(listBox, dropdown) {
    var selectedItems = listBox.GetSelectedItems();
    dropdown.SetText(GetSelectedItemsText(selectedItems));
}
function SynchronizeListBoxValues(dropDown, args, listbox) {
    listbox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts, listbox);
    listbox.SelectValues(values);
    UpdateSelectAllItemState(listbox);
    UpdateText(listbox, dropDown); // for remove non-existing texts
}
function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)

        texts.push(items[i].value);
    return texts.join(textSeparator);
}
function GetValuesByTexts(texts, listbox) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = listbox.FindItemByValue(texts[i]);

        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}