import './globals.css';
import type { Metadata } from 'next';
import { Layout } from '../components/layout/Layout';

export const metadata: Metadata = {
  title: 'المتجر العربي - أفضل منصة للتسوق الإلكتروني',
  description: 'اكتشف أفضل المنتجات بأسعار تنافسية مع توصيل مجاني وضمان الجودة في المتجر العربي',
  keywords: 'تسوق إلكتروني، منتجات عربية، توصيل مجاني، دينار عراقي، زين كاش',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="ar" dir="rtl">
      <body className="bg-gray-50 text-gray-900 font-sans antialiased">
        <Layout>
          {children}
        </Layout>
      </body>
    </html>
  );
}