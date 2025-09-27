import './globals.css';
import type { Metadata } from 'next';
import { dir } from 'i18next';

export const metadata: Metadata = {
  title: 'متجر إلكتروني',
  description: 'تجربة تسوق حديثة بواجهة عربية كاملة',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="ar" dir="rtl">
      <body className="bg-white text-gray-900 font-sans">
        {children}
      </body>
    </html>
  );
}