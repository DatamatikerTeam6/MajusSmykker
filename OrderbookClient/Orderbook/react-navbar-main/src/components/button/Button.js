import React from "react";
import PropTypes from "prop-types";



const Button = ({
  children,
  className = "",
  leftIcon,
  rightIcon,
  shape = "round",
  variant = "fill",
  size = "xs",
  color = "cyan_100",
  ...restProps
}) => {
  const shapes = {
    round: "rounded",
  };

  const variants = {
    fill: {
      cyan_100: "bg-cyan-100 text-black-900",
    },
  };

  const sizes = {
    xs: "h-[64px] px-[30px] text-[20px]",
  };

  return (
    <button
      className={`${className} ${shapes[shape]} ${variants[variant][color]} ${sizes[size]} flex flex-row items-center justify-center md:ml-0 sm:px-4 text-center cursor-pointer whitespace-nowrap text-black-900`}
      {...restProps}
    >
      {!!leftIcon && leftIcon}
      {children}
      {!!rightIcon && rightIcon}
    </button>
  );
};

Button.propTypes = {
  className: PropTypes.string,
  children: PropTypes.node,
  leftIcon: PropTypes.node,
  rightIcon: PropTypes.node,
  shape: PropTypes.oneOf(["round"]),
  size: PropTypes.oneOf(["xs"]),
  variant: PropTypes.oneOf(["fill"]),
  color: PropTypes.oneOf(["cyan_100"]),
};

export { Button };
