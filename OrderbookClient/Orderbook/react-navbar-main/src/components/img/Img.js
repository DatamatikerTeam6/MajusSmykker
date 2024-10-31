import React from "react";

const Img = ({ className, src = "defaultNoData.png", alt = "testing", ...restProps }) => {
    return (
        <img
            className={className}
            src={src}
            alt={alt}
            {...restProps} // Corrected spread operator usage
            loading="lazy"
        />
    );
};

export { Img }; // Moved export statement outside the component definition
