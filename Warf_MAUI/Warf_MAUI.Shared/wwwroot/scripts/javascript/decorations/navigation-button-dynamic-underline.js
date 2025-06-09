"use strict";
const decorationClassName = 'wa-interactive-underline';
const navigationItem = document
    .getElementsByTagName('nav').item(0);
console.log(navigationItem);
if (navigationItem) {
    var decoration_underline = navigationItem.getElementsByClassName(decorationClassName);
    console.log(decoration_underline);
    if (decoration_underline == null || decoration_underline == undefined)
        throw new Error(`Cannot find elments with class '${decorationClassName}'`);
    for (var index = 0; index < decoration_underline.length; index++) {
        let decorationElement = decoration_underline[index];
        if (!decorationElement)
            throw new Error("decorationElement was null in navigation decoration" +
                "underline elements list.");
        let decorationParent = decoration_underline[index].parentElement;
        if (!decorationParent)
            throw new Error("decorationParent was null for decoration underline element.");
        decorationParent.addEventListener('mouseover', function (ev) { ShowDecoration(decorationElement, ev, this); });
        decorationParent.addEventListener('mouseleave', function (ev) { HideDecoration(decorationElement, ev, this); });
    }
}
function ShowDecoration(decorationElement, ev, linkElement) {
    decorationElement.style.width = `${linkElement.clientWidth}px`;
}
function HideDecoration(decorationElement, ev, linkElement) {
    decorationElement.style.width = `0px`;
}
//# sourceMappingURL=navigation-button-dynamic-underline.js.map