import React from 'react';
import { Button } from '../ui';

export const NewsletterSection: React.FC = () => {
  return (
    <section className="py-16 bg-gradient-to-r from-blue-600 to-purple-700 text-white">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center">
          <div className="mb-8">
            <EmailIcon className="h-16 w-16 mx-auto text-yellow-400 mb-4" />
            <h2 className="text-3xl lg:text-4xl font-bold mb-4">
              اشترك في النشرة الإخبارية
            </h2>
            <p className="text-xl text-blue-100 max-w-2xl mx-auto">
              احصل على آخر العروض والمنتجات الجديدة مباشرة في صندوق البريد الخاص بك
            </p>
          </div>

          <div className="max-w-md mx-auto">
            <div className="flex flex-col sm:flex-row gap-4">
              <input
                type="email"
                placeholder="أدخل بريدك الإلكتروني"
                className="flex-1 px-4 py-3 rounded-lg border border-transparent focus:border-white focus:ring-2 focus:ring-white focus:ring-opacity-50 text-gray-900 placeholder-gray-500"
              />
              <Button 
                size="lg" 
                className="bg-yellow-400 hover:bg-yellow-500 text-gray-900 font-bold whitespace-nowrap"
              >
                اشتراك
              </Button>
            </div>
            <p className="text-sm text-blue-200 mt-3">
              يمكنك إلغاء الاشتراك في أي وقت. نحترم خصوصيتك ولن نرسل لك رسائل مزعجة.
            </p>
          </div>

          {/* Benefits */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mt-12">
            <div className="flex items-center justify-center space-x-3 space-x-reverse">
              <OfferIcon className="h-6 w-6 text-yellow-400 flex-shrink-0" />
              <span className="text-sm">عروض حصرية</span>
            </div>
            <div className="flex items-center justify-center space-x-3 space-x-reverse">
              <NewIcon className="h-6 w-6 text-yellow-400 flex-shrink-0" />
              <span className="text-sm">المنتجات الجديدة</span>
            </div>
            <div className="flex items-center justify-center space-x-3 space-x-reverse">
              <TipsIcon className="h-6 w-6 text-yellow-400 flex-shrink-0" />
              <span className="text-sm">نصائح التسوق</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

// Icons
const EmailIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M21.75 6.75v10.5a2.25 2.25 0 01-2.25 2.25h-15a2.25 2.25 0 01-2.25-2.25V6.75m19.5 0A2.25 2.25 0 0019.5 4.5h-15a2.25 2.25 0 00-2.25 2.25m19.5 0v.243a2.25 2.25 0 01-1.07 1.916l-7.5 4.615a2.25 2.25 0 01-2.36 0L3.32 8.91a2.25 2.25 0 01-1.07-1.916V6.75" />
  </svg>
);

const OfferIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M9.568 3H5.25A2.25 2.25 0 003 5.25v4.318c0 .597.237 1.17.659 1.591l9.581 9.581c.699.699 1.78.872 2.607.33a18.095 18.095 0 005.223-5.223c.542-.827.369-1.908-.33-2.607L11.16 3.66A2.25 2.25 0 009.568 3z" />
    <path strokeLinecap="round" strokeLinejoin="round" d="M6 6h.008v.008H6V6z" />
  </svg>
);

const NewIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M9.813 15.904L9 18.75l-.813-2.846a4.5 4.5 0 00-3.09-3.09L2.25 12l2.846-.813a4.5 4.5 0 003.09-3.09L9 5.25l.813 2.846a4.5 4.5 0 003.09 3.09L15.75 12l-2.846.813a4.5 4.5 0 00-3.09 3.09zM18.259 8.715L18 9.75l-.259-1.035a3.375 3.375 0 00-2.455-2.456L14.25 6l1.036-.259a3.375 3.375 0 002.455-2.456L18 2.25l.259 1.035a3.375 3.375 0 002.456 2.456L21.75 6l-1.035.259a3.375 3.375 0 00-2.456 2.456zM16.894 20.567L16.5 21.75l-.394-1.183a2.25 2.25 0 00-1.423-1.423L13.5 18.75l1.183-.394a2.25 2.25 0 001.423-1.423l.394-1.183.394 1.183a2.25 2.25 0 001.423 1.423l1.183.394-1.183.394a2.25 2.25 0 00-1.423 1.423z" />
  </svg>
);

const TipsIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 001.5-.189 6.044 6.044 0 01.39 1.679 5.25 5.25 0 11-3.78 0c.133-.564.28-1.122.46-1.679A6.01 6.01 0 0012 12.75zM12 9.75a3 3 0 100-6 3 3 0 000 6z" />
  </svg>
);