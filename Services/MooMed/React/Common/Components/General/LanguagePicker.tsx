// Framework
import * as React from "react";
import * as moment from "moment";

// Components
import Flex from "Common/Components/Flex";

// Functionality
import * as cookieHelper from "helper/cookieHelper";

import "./Styles/LanguagePicker.less";

export const LanguagePicker: React.FC = () => {

    const [currentLang, setCurrentLang] = React.useState("en");

    const pickLang = React.useCallback((lang: string, shouldReload = true) => {
        cookieHelper.setCookie({
            name: "lang",
            value: lang,
            expirationDate: moment().add(9999, 'days').format("ddd, DD MMM YYYY HH:mm:ss")
        });

        if (shouldReload) {
            location.reload();
        }
    }, []);

    React.useEffect(() => {
        const existingCookie = cookieHelper.getCookie("lang");

        if (existingCookie) {
            setCurrentLang(existingCookie.value);
        }
    }, []);

    return (
        <Flex className="languagePickerContainer">
            <Flex 
                className={currentLang === "en" ? "languagePickerEn highlight" : "languagePickerEn"} 
                onClick={() => pickLang("en")}>
            </Flex>
            <Flex 
                className={currentLang === "de" ? "languagePickerDe highlight" : "languagePickerDe"} 
                onClick={() => pickLang("de")}>
            </Flex>
        </Flex>
    );
}

export default LanguagePicker;