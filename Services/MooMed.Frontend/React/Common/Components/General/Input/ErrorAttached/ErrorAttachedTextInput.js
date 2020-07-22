"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const timers_1 = require("timers");
const Flex_1 = require("Common/Components/Flex");
exports.ErrorAttachedTextInput = ({ name, inputType, payload: propPayload, onChangeFunc, errorFunc, errorMessage, onEnterPress }) => {
    let touchTimeout;
    const [touched, setTouched] = React.useState(false);
    const [payload, setPayload] = React.useState(propPayload);
    const [isValid, setIsValid] = React.useState(true);
    const calculateValidity = React.useCallback((data) => {
        let validity = true;
        if (touched) {
            if (errorFunc) {
                validity = !errorFunc(data);
            }
        }
        setIsValid(validity);
        return validity;
    }, [touched, errorFunc]);
    const handleChange = React.useCallback((event) => {
        const val = event.target.value;
        setPayload(val);
        const validity = calculateValidity(val);
        onChangeFunc(val, validity);
        if (!touched) {
            if (touchTimeout) {
                clearTimeout(touchTimeout);
            }
            touchTimeout = timers_1.setTimeout(() => {
                setTouched(true);
            }, 500);
        }
    }, [touched]);
    const handleKeyPress = React.useCallback((event) => {
        if (event.charCode === 13 && typeof onEnterPress !== "undefined") {
            onEnterPress();
        }
    }, [onEnterPress]);
    return (React.createElement(Flex_1.default, { direction: "Column", className: "form-group" },
        React.createElement("input", { className: (touched && !isValid) ? "form-control is-invalid" : "form-control", type: inputType != null ? inputType : "text", name: name, value: payload, onChange: handleChange, placeholder: name, onKeyPress: handleKeyPress }),
        React.createElement(If, { condition: touched && !isValid && errorMessage !== "" },
            React.createElement(Flex_1.default, { className: "invalid-feedback" }, errorMessage))));
};
exports.default = exports.ErrorAttachedTextInput;
//# sourceMappingURL=ErrorAttachedTextInput.js.map