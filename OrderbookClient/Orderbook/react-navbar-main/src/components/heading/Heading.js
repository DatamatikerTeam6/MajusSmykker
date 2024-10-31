import React from "react";

const sizes = {
  textxs: "text-[20px] font-medium lg:text-[17px]",
  texts: "text-[64px] font-medium lg:text-[48px] md:text-[48px]",
};

const Heading = ({ children, className = "", size = "textxs", as, ...restProps }) => {
  const Component = as || "h6";

  return (
    <Component className={`text-black-900 font-librefranklin ${className} ${sizes[size]}`} {...restProps}>
      {children}
    </Component>
  );
};

export { Heading };
