import { Link } from "react-router-dom"; // Use Link for navigation
import { Heading } from "./components/heading/Heading";
import { Img } from "./components/img/Img"; 
import React from "react";


export default function Header({ ...props }) {
    return (
        <header
            {...props}
            className={`${props.className} flex sm:flex-col self-stretch justify-between items-center gap-5`} // Fixed string interpolation
        >
            <ul className="flex flex-wrap items-center gap-7">
                <li>
                    <a href="#" className="lg:text-[30px] md:text-[30px] sm:text-[28px]"> {/* Corrected class names */}
                        <Heading size="texts" as="p" className="text-[36px] font-medium tracking-[-0.79px] text-gray-700">
                            Menu
                        </Heading>
                    </a>
                </li>
                <li>
                    <a href="./Customer" className="lg:text-[17px]">
                        <Heading as="p" className="text-[20px] font-medium tracking-[-0.44px] text-gray-700">
                            Kunder
                        </Heading>
                    </a>
                </li>
                <li>
                    <a href="./Order" className="lg:text-[17px]">
                        <Heading as="p" className="text-[20px] font-medium tracking-[-0.44px] text-gray-700">
                            Ordre
                        </Heading>
                    </a>
                </li>
                <li>
                    <a href="./Review" className="lg:text-[17px]">
                        <Heading as="p" className="text-[20px] font-medium tracking-[-0.44px] text-gray-700">
                        Swinging the penis
                        </Heading>
                    </a>
                </li>
            </ul>
            <a href="#">
                <Img src="images/img_thumbs_up.png" alt="Thumbs Up Image" className="h-[24px] w-[24px] sm:w-full" /> {/* Used Img component */}
            </a>
        </header>
    );
}

