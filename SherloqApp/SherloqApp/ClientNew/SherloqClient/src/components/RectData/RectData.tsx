import { AppTypography } from "@/styles/global";
import { RectDataContainer } from "./RectData.styled";
import { TypographyBold, TypographySize } from "@/types/styles.d";
import React from "react";

interface RectDataProps {
  title: string;
  value: string;
  valueColor: string;
}
const RectData: React.FC<RectDataProps> = ({ value, title, valueColor }) => {
  return (
    <RectDataContainer>
      <AppTypography textColor={valueColor} size={TypographySize.lg} bold={TypographyBold.lg}>
        {value}
      </AppTypography>
      <AppTypography wrap>{title}</AppTypography>
    </RectDataContainer>
  );
};

export default RectData;
