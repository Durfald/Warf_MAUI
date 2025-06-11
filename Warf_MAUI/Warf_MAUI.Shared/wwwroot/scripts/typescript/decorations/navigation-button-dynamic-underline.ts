const decorationClassName = 'wa-interactive-underline';
interface INavigationButtonDecoration {
    initialWidth: number,
    decorationElement: HTMLElement,
    decorationParent: HTMLElement
};
var navigationButtonDecorations: Array<INavigationButtonDecoration> = [];

const navigationItem = document
    .getElementsByTagName('nav').item(0);

if (navigationItem) {
    var decoration_underline: HTMLCollectionOf<HTMLDivElement> | null = navigationItem
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
