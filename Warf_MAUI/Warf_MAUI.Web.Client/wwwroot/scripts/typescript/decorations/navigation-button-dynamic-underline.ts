const decorationClassName = 'wa-interactive-underline';
const decorationForcedInitialWidthAttributeName = 'wa-decoration-underline-forced-initial-width';

interface INavigationButtonDecoration {
    initialWidth: number,
    decorationElement: HTMLElement,
    decorationParent: HTMLElement
};
var navigationButtonDecorations: Array<INavigationButtonDecoration> = [];

const navigationItemContainers = document.getElementsByClassName('wa-navigation-container');

if (navigationItemContainers) {
    for (let navigationItemIndex = 0; navigationItemIndex < navigationItemContainers.length; navigationItemIndex++) {
        let navigationItemContainer = navigationItemContainers[navigationItemIndex];

        var decoration_underline: HTMLCollectionOf<HTMLDivElement> | null = navigationItemContainer
            .getElementsByClassName(decorationClassName) as HTMLCollectionOf<HTMLDivElement>;
        if (decoration_underline == null || decoration_underline == undefined)
            throw new Error(`Cannot find elments with class '${decorationClassName}'`);

        for (var index = 0; index < decoration_underline.length; index++) {
            // полоска
            let decorationElement = decoration_underline[index] as HTMLDivElement;
            if (!decorationElement)
                throw new Error("decorationElement was null in navigation decoration" +
                    "underline elements list.");

            // <a>, к которому биндится выдвижение полоски
            let decorationParent = decoration_underline[index].parentElement as HTMLElement;
            if (!decorationParent)
                throw new Error("decorationParent was null for decoration underline element.");

            // контейнер, чтобы зафиксировать длину кнопки
            let buttonDecoration: INavigationButtonDecoration = {
                initialWidth: decorationParent.clientWidth,
                decorationElement: decorationElement,
                decorationParent: decorationParent
            };

            if (buttonDecoration.initialWidth == 0) {
                buttonDecoration.initialWidth = parseInt(buttonDecoration.decorationParent.getAttribute(
                    decorationForcedInitialWidthAttributeName) ?? '0');
            }

            // складирую, чтобы не диспозились
            navigationButtonDecorations.push(buttonDecoration);

            buttonDecoration.decorationParent.addEventListener('mouseover', function (ev) {
                ShowDecoration(ev, navigationButtonDecorations[
                    navigationButtonDecorations.indexOf(buttonDecoration)])
            });

            buttonDecoration.decorationParent.addEventListener('mouseleave', function (ev) {
                HideDecoration(ev, navigationButtonDecorations[
                    navigationButtonDecorations.indexOf(buttonDecoration)])
            });

            buttonDecoration.decorationParent.addEventListener('click',
                function (ev: MouseEvent) {
                    navigationButtonDecorations.forEach(x => HideDecoration(ev, x, true))
                    ShowDecoration(ev, buttonDecoration);
                });
        }
    }
}

function ShowDecoration(ev: MouseEvent, buttonDecoration: INavigationButtonDecoration) {
    buttonDecoration.decorationElement.style.width = `${buttonDecoration.initialWidth}px`;
}

function HideDecoration(
    ev: MouseEvent,
    buttonDecoration: INavigationButtonDecoration,
    forced: boolean = false) {
    if (!buttonDecoration.decorationParent.classList.contains('active') || forced)
        buttonDecoration.decorationElement.style.width = `0px`;
}
