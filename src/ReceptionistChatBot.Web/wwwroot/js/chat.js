window.receptionistChat = {
    scrollToLatest: function (elementId) {
        const element = document.getElementById(elementId);

        if (!element) {
            return;
        }

        element.scrollTo({
            top: element.scrollHeight,
            behavior: "smooth"
        });
    },

    focusInput: function (elementId) {
        const element = document.getElementById(elementId);

        if (!element) {
            return;
        }

        setTimeout(function () {
            element.focus();
        }, 100);
    }
};
