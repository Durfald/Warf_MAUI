function loadItemImage(image: HTMLImageElement, itemId: string, language: string): void {
    const imageUrl = `/info/details/${itemId}`;
    try {

        fetch('http://37.46.19.255:2530/api/' + imageUrl, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Language': language
            }
        })
            .then(response => {
                response.json().then(data => {
                    console.log(data);
                    //image.src = data.i18n["ru"].icon;
                })
            });
    } catch (error) {
        console.log(error)
    }
}